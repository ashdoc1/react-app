import React, { useState, useEffect } from 'react';
import { io } from 'socket.io-client';

const PizzaList = () => {
  const [menuItems, setMenuItems] = useState([]);
  const [isWsConnected, setIsWsConnected] = useState(false);

  useEffect(() => {
    const socket = io('ws://www.agstyahomes.com/ws-menu');

    socket.on('connect', () => {
      console.log('Connected to WebSocket server');
      setIsWsConnected(true);
    });

    socket.on('message', (data) => {
      const parsedData = JSON.parse(data);
      setMenuItems(parsedData.items);
    });

    return () => {
      socket.disconnect();
    };
  }, []);

  const handleWsGetPizzaList = () => {
    // Send a message to the server to request the pizza list
    const socket = io('ws://www.agstyahomes.com/ws-menu');
    socket.emit('getPizzaList');
  };

  return (
    <div className="card">
      <h1 className="heading">Pizza Menu</h1>
      <ul>
        {menuItems.map((item, index) => (
          <li key={index}>{item}</li>
        ))}
      </ul>
      <button onClick={handleWsGetPizzaList} disabled={isWsConnected}>
        Get Pizza List on WS
      </button>
    </div>
  );
};

export default PizzaList;
