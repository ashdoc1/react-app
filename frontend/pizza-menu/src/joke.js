import React, { useState, useEffect } from 'react';
import axios from 'axios';

const Joke = () => {
  const [joke, setJoke] = useState({});

  useEffect(() => {
    const fetchJoke = async () => {
      const response = await axios.get('https://official-joke-api.appspot.com/random_joke');
      setJoke(response.data);
    };

    fetchJoke();
  }, []);

  return (
    <div className="card">
      <h1 className="heading">Random Joke</h1>
      <p>Type: {joke.type}</p>
      <p>Setup: {joke.setup}</p>
      <p>Punchline: {joke.punchline}</p>
    </div>
  );
};

export default Joke;