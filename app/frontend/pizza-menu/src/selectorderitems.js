import React from 'react';

function PizzaOrder({ addToCart }) {
  const pizzaList = [
    { name: "Margherita", price: 10, id: "MRGRT" },
    { name: "Pepperoni", price: 12, id: "PEPRN"  },
    { name: "Veggie", price: 11, id: "VEG"  },
    { name: "Hawaiian", price: 13, id: "HAWAN"  },
    { name: "BBQ Chicken", price: 14, id: "BBQ"  },
    { name: "Indian Chicken", price: 15, id: "ICHN"  },
  ];

  return (
    <div>
      <h1 className="heading">Prepare your Special Pizza</h1>
      <div className="table-container">
        <table>
          <thead>
            <tr>
              <th>Pizza</th>
              <th>Price</th>
              <th>Quantity</th>
              <th>Add to Cart</th>
            </tr>
          </thead>
          <tbody>
            {pizzaList.map((pizza, index) => (
              <tr key={index}>
                <td>{pizza.name}</td>
                <td>£{pizza.price}</td>
                <td>
                  <div className="quantity-container">
                    <input id={`quantity-${pizza.id}`} type="number" min="1" defaultValue="1" />
                  </div>
                </td>
                <td>
                  <button onClick={() => addToCart(pizza.id, pizza.name, document.getElementById(`quantity-${pizza.id}`).value)}>
                    +
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}

export default PizzaOrder;

// import React, { useState } from "react";
// //import PlaceOrder from "./PlaceOrder";
// //import Cart from "../Components/Cart";

// function PizzaOrder() {
//   const [cart, setCart] = useState([]);
//   const pizzaList = [
//     { name: "Margherita", price: 10, id: "MRGRT" },
//     { name: "Pepperoni", price: 12, id: "PEPRN"  },
//     { name: "Veggie", price: 11, id: "VEG"  },
//     { name: "Hawaiian", price: 13, id: "HAWAN"  },
//     { name: "BBQ Chicken", price: 14, id: "BBQ"  },
//     { name: "Indian Chicken", price: 15, id: "ICHN"  },
//   ];

//   const AddToCart = (pizzaId,pizzaName,pizzaQuantity) => {
//     console.log(`---1-cart---${cart.length}-------`);
//     console.log(`-----pizza-->--${pizzaName} ${pizzaQuantity}-------`);
//     // debugger
//     setCart((cart) => [...cart, {pizzaId,pizzaName,pizzaQuantity} ]);
//     console.log(`----2---${typeof(cart)}--${cart.length}-----`);
//     for (let i = 0; i < cart.length; i++) {
//       console.log(cart[i].pizzaName);
//       console.log(cart[i].pizzaQuantity);
//     }
//   };


// //};
//   return (
//     <div>
//       <h1 className="heading">Prepare your Special Pizza</h1>
//       <div className="table-container">
//         <table>
//           <thead>
//             <tr>
//               <th>Pizza</th>
//               <th>Price</th>
//               <th>Quantity</th>
//               <th>Add to Cart</th>
//             </tr>
//           </thead>
//           <tbody>
//             {pizzaList.map((pizza, index) => (
//               <tr key={index}>
//                 <td>{pizza.name}</td>
//                 <td>£{pizza.price}</td>
//                 <td>
//                   <div className="quantity-container">
//                     <input id="quantity" type="number" min="1" defaultValue="1" />
//                   </div>
//                 </td>
//                 <td>
//                   <button onClick={() => AddToCart(pizza.id, pizza.name, "1")}>
//                     +
//                   </button>
//                 </td>
//               </tr>
//             ))}
//           </tbody>
//         </table>
//       </div>
//     </div>
//   );
// }

// export default PizzaOrder;