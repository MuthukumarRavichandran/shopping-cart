import  { useState, useEffect, Fragment } from 'react';
import { MdDelete } from "react-icons/md";
import axios from "axios";

import Cart from '../Cart/Cart';
import Button from '../../modules/Button/Button';

import "./CartQuantitySelector.scss";

const data = ['apple','orange','banana'];

function CartQuantitySelector(){

    const [apiResponse, setApiResponse] = useState([]);
    const [loading, setLoading] = useState(false);
    const[count, setCount] = useState(0);
    const [selectedItem, setSelectedItem] = useState("apple");
    const [sessionId, setsessionId] = useState("");
    const [checkoutTrigger, setCheckoutTrigger] = useState(0);
    const [errorMessage, setErrorMessage] = useState("");

    useEffect(() => {
        generateNewSessionId();
    },[])

     useEffect(() => {      
         if (checkoutTrigger === 0) return;
        const baseAddress = 'https://localhost:7061'
        const url = baseAddress + `/api/cart?sessionId=${sessionId}&quantity=${count}&item=${selectedItem}`;
        setLoading(true);
        axios
            .get(url)
            .then((response) => {
                setApiResponse(response.data);
                setLoading(false);
            })
            .catch((error) => {
                console.log(error);
                 let message = "Failed to update cart. Please try again.";
                 if (error.response) {
                  message = error.response.data?.error || error.response.statusText;
                }
                setErrorMessage(message);
                setTimeout(() => setErrorMessage(""), 3000);
                setLoading(false);
            });
    }, [checkoutTrigger, sessionId, count, selectedItem]);

  const generateNewSessionId = () => {
    const newSessionId = "session-" + Math.random().toString(36).substring(2, 10);
    setsessionId(newSessionId);
  };

    const increment = () =>{
        setCheckoutTrigger(0);
        setCount(prevcount => prevcount +1);
    }
    const decrement = () => {
        setCheckoutTrigger(0);
        setCount(prevcount => prevcount -1);
    }

    const handleCheckout = () => {
        if(count <= 0){
            alert("Please select something");
            return;
        }
        setCheckoutTrigger(1);
    }
    
    const handleClearAll = () => {
        setCount(0);
        setApiResponse([]);
        generateNewSessionId();
        setLoading(false);
        setCheckoutTrigger(0);
    };

    return(
        <Fragment>
          <div className="cart-controls">
              <div className="select-box">
                <span>Select Item</span>
                <select
                    value={selectedItem}
                    onChange={(e) => {
                      setSelectedItem(e.target.value);
                      setCheckoutTrigger(0);
                      setCount(0);}}>
                        {data.map((item) => (
                        <option key={item} value={item}>{item}</option>
                        ))}
                  </select>
              </div>
              <div className="quantity-box">
                <span>Quantity</span>
                {count === 0 ? (
                  <Button onClick={increment}>Add</Button>
                ) : 
                  (<div className="quantity-buttons">
                    <Button onClick={decrement}>
                      {count === 1 ? <MdDelete size={18} /> : "-"}
                    </Button>
                    <span>{count}</span>
                    <Button onClick={increment}>+</Button>
                  </div>
                  )
                }
              </div>
              <Button type='success' className="checkout-button" onClick={handleCheckout}>
                Checkout
              </Button>
              <Button type='danger' className="clear-button" onClick={handleClearAll}>
                Clear All
              </Button>
              {errorMessage && (<div className="error-message">{errorMessage}</div>)}             
          </div>
          <Cart apiResponse={apiResponse} loading={loading}/>
         </Fragment>
    )
}

export default CartQuantitySelector;