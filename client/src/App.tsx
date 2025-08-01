import { useState, useEffect } from 'react';
import axios from 'axios';
import LoginPage from './pages/LoginPage';

function App() {

  const[user, setUser] = useState<User | null>(null);

  useEffect(() => {
    const token = localStorage.getItem('token');
    const userId = localStorage.getItem('userId');
    
    if(token && userId) {
      axios.get<User>('https://localhost:5001/api/auth/', {
        headers: {
          Authorization: `Bearer ${token}`
        }
      }).then( async () => {
        const user = await axios.get<User>(`https://localhost:5001/api/user/${userId}`);
        setUser(user.data);
        console.log(user.data);
      }).catch((error) => {
        console.error('Error fetching user:', error);
        localStorage.removeItem('token');
        localStorage.removeItem('userId');
        setUser(null);
      });
    }
  }, []);

  const handleLogout = () => {
    localStorage.removeItem('token');
    localStorage.removeItem('userId');
    setUser(null);
  };

  return (
    <>
      {user ? (
        <div>
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
