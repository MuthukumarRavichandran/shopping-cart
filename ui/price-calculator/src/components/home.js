import CartQuantitySelector from './CartQuantitySelector/CartQuantitySelector';

function Home() {
    return (
        <div>
            <h3 style={{marginLeft:"20px", marginTop:"10px", color:"#160230ff"}}>Cart</h3>
            <CartQuantitySelector />
        </div>
        
    )
}

export default Home;