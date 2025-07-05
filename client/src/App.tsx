import { useState, useEffect } from 'react';
import axios from 'axios';

function App() {

  const[user, setUser] = useState<User[]>([]);

  useEffect(() => {
  axios.get<User[]>('https://localhost:5001/api/user').then((response) => {
    setUser(response.data);
  });
  }, []);

  return (
    <>
      <h1>Aircraft Doc App</h1>
      <ul>
        {user.map((user) => (
          <li key={user.id}>{user.username}</li>
        ))}
      </ul>
    </>
  )
}

export default App
