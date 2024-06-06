import React,{ useState } from 'react';

function Cart({ cart }) {
  const aggregatedCart = {};

  // Aggregate quantities for each pizzaId
  cart.forEach((item) => {
    if (aggregatedCart[item.pizzaId]) {
      aggregatedCart[item.pizzaId].quantity = parseInt(aggregatedCart[item.pizzaId].quantity)+parseInt(item.pizzaQuantity);
    } else {
      aggregatedCart[item.pizzaId] = { ...item, quantity: item.pizzaQuantity };
    }
  });

  // Convert aggregated cart to JSON message
  const jsonMessage = JSON.stringify(Object.values(aggregatedCart));

  const [isOrderPlaced, setIsOrderPlaced] = useState(false);

  const handlePlaceOrder = async () => {
    try {
      const response = await fetch('http://www.agstyahomes.com/api/orders', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: jsonMessage,
      });

      if (response.ok) {
        setIsOrderPlaced(true);
      } else {
        console.error('Error placing order:', await response.text());
      }
    } catch (error) {
      console.error('Error placing order:', error);
    }
  };

  return (
    <div>
      <h2>Cart</h2>
      {cart.length === 0 ? (
        <p>No items i n cart.</p>
      ) : (
        <ul>
          {Object.values(aggregatedCart).map((item, index) => (
            <li key={index}>
              {item.pizzaName} x {item.quantity}
            </li>
          ))}
        </ul>
      )}
      <p>JSON Message: {jsonMessage}</p>
      {!isOrderPlaced && (
        <button onClick={handlePlaceOrder}>Place Order</button>
      )}
      {isOrderPlaced && <p>Order placed successfully!</p>}
    </div>
  );
}

export default Cart;