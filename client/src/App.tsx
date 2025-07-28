import { useState, useEffect } from 'react';
import axios from 'axios';
import LoginPage from './pages/LoginPage';

function App() {

  const[user, setUser] = useState<User | null>(null);

  useEffect(() => {
    const token = localStorage.getItem('token');
    if(token) {
      axios.get<User>('https://localhost:5001/api/auth/', {
        headers: {
          Authorization: `Bearer ${token}`
        }
      }).then((response) => {
        setUser(response.data);
      }).catch((error) => {
        console.error('Error fetching user:', error);
        // If token is invalid, remove it
        localStorage.removeItem('token');
        setUser(null);
      });
    }
  }, []);

  const handleLogout = () => {
    localStorage.removeItem('token');
    setUser(null);
  };

  return (
    <>
      {user ? (
        <div>
          <h1>Aircraft Doc App</h1>
          <p>Welcome, {user.name}!</p>
          <button onClick={handleLogout}>Logout</button>
        </div>
      ) : (
        <LoginPage />
      )}
    </>
  )
}

export default App
