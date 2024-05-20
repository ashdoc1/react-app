// import React, { useState, useEffect } from 'react';
// import axios from 'axios';
import Joke from './joke';
import PizzaList from './pizza_list';

const App = () => {
    return (
        <div>
            <div>
                <Joke />
            </div>
            <div>
                <PizzaList />
            </div>
        </div>
    )
}
export default App;