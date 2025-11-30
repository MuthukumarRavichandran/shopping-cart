import Table from 'react-bootstrap/Table';

const tableHeadings = ['S.No,', 'Iteam Name', 'Quantity', 'Rate', 'Discount', 'Final Amount']

function Cart ({apiResponse, loading}){

    const totalPrice = apiResponse.reduce((accumulator, currentValue) => accumulator + currentValue.price, 0);
    return(
    <Table striped bordered hover style={{marginLeft:"20px", width:"90%"}}>
      <thead>
        <tr>
          {tableHeadings.map(
            (heading) => (<td key={heading}>{heading}</td>)
        )}
        </tr>
      </thead>
      <tbody>
    {
    loading ? (
      <tr>
        <td colSpan={6}>Data loading</td>
      </tr>
    ) : (
      apiResponse.length === 0 ? (
        <tr>
          <td colSpan={6}>No data to display</td>
        </tr>
      ) : (
        <>
          {apiResponse.map(({ item, quantity, rate, discount, price }, index) => {
            const itemNo = index + 1;
            return (
              <tr key={itemNo}>
                <td>{itemNo}</td>
                <td>{item}</td>
                <td>{quantity}</td>
                <td>{rate}</td>
                <td>{discount}%</td>
                <td>{price}</td>
              </tr>
            );
          })}

          <tr>
            <td colSpan={5}>Total Amount</td>
            <td>{totalPrice}</td>
          </tr>
        </>
      )
    )
  }
</tbody>     
    </Table>
  );
}

export default Cart;