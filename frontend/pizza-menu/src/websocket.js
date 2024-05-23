// src/PizzaMenu.js
import React, { useState, useEffect } from 'react';
import useWebSocket from 'react-use-websocket';

const PizzaMenu = () => {
  const [menuItems, setMenuItems] = useState([]);
  const { sendMessage, lastMessage, readyState } = useWebSocket('ws://www.agstyahomes.com/ws-menu', {
    onOpen: () => console.log('Connected to WebSocket'),
    onClose: () => console.log('Disconnected from WebSocket'),
    onError: (error) => console.error('WebSocket Error:', error),
    shouldReconnect: (closeEvent) => true, // Reconnect on close
  });

  useEffect(() => {
    if (lastMessage !== null) {
      const data = JSON.parse(lastMessage.data);
      setMenuItems(data.items);
    }
  }, [lastMessage]);

  return (
    <div className="card">
      <h1 className="heading">Pizza Menu</h1>
      {readyState === WebSocket.OPEN ? (
        <ul>
          {menuItems.map((item, index) => (
            <li key={index}>{item}</li>
          ))}
        </ul>
      ) : (
        <p>Connecting to WebSocket...</p>
      )}
    </div>
  );
};

export default PizzaMenu;
