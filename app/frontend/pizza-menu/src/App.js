import Joke from './joke';
//import Notification from  './notification'
import React, { useState } from 'react';
import PizzaOrder from './selectorderitems';
import Cart from './cart';

function App() {
    const [cart, setCart] = useState([]);
  
    const AddToCart = (pizzaId, pizzaName, pizzaQuantity) => {
      setCart((prevCart) => [...prevCart, { pizzaId, pizzaName, pizzaQuantity }]);
    };
  
    return (
      <div>
        <h1>Pizza Order App</h1>
        <PizzaOrder addToCart={AddToCart} />
        <Cart cart={cart} />
      </div>
    );
  }
export default App;

// import Joke from './joke';
// import Notification from  './notification'

// const App = () => {
//     return (
//         <div>
//             <div>
//                 <Joke />
//             </div>
//             <div>
//                 <Notification />
//             </div>
//         </div>
//     )
// }
// export default App;