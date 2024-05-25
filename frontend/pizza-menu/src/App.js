// import React, { useState, useEffect } from 'react';
// import axios from 'axios';
import Joke from './joke';
// import PizzaList from './pizza_list';
import PizzaMenu from  './pizzamenu'

const App = () => {
    return (
        <div>
            <div>
                <Joke />
            </div>
            <div>
                <PizzaMenu />
            </div>
        </div>
    )
}
export default App;