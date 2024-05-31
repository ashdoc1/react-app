// src/Notification.js
import React, { useState, useEffect } from 'react';
import useWebSocket from 'react-use-websocket';

const Notification = () => {
  const [menuItem, setMenuItem] = useState(''); // Use an array to store menu items
  const { lastMessage, readyState } = useWebSocket('ws://www.agstyahomes.com/ws-notif', {
  //const {lastMessage, readyState } = useWebSocket('ws://www.agstyahomes.com/ws-notif', {
    onOpen: () => console.log('Connected to WebSocket'),
    onClose: () => console.log('Disconnected from WebSocket'),
    onError: (error) => console.error('WebSocket Error:', error),
    shouldReconnect: (closeEvent) => true, // Reconnect on close
  });

  useEffect(() => {
    if (lastMessage !== null) {
      try {
        console.log("typeof lastMessage:", typeof lastMessage); 
        const data = JSON.parse(lastMessage.data);
        console.log("typeof data:", typeof data); 
        console.log("Received data 2:", data);
        console.log("Received data 2:", lastMessage);
       // setMenuItems([data.item]); lastMessage.data
        setMenuItem(data);
        console.log("---------3----");          
      } catch (error) {
        console.log("-------error--3----");        
        console.error("Error parsing data:", error);
      }      
    }
  }, [lastMessage]);

 

  return (
    <div className="card">
      <h1 className="heading">Notifications</h1>
      {readyState === WebSocket.OPEN ? (
        <ul>
          {/* {menuItems.map((item, index) => (
            <li key={index}>{item}</li>
          ))} */}
          <li>{menuItem === undefined ? "UNDEFINED" : menuItem}</li>
        </ul>
      ) : (
        <p>Connecting to WebSocket...</p>
      )
      }
    </div>
  );
};

export default Notification;
