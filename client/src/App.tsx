import { useState, useEffect } from 'react';
import axios from 'axios';
import LoginPage from './pages/LoginPage';
import NavBar from './components/NavBar';

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
        const user = await axios.get<User>(`https://localhost:5001/api/users/${userId}`);
        setUser(user.data);
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
      <NavBar />
      <div className="container mx-full px-4 pt-20 flex justify-center items-center h-screen w-screen">
        {user ? (
          <div>
            <p>Welcome, {user.name}!</p>
            <button onClick={handleLogout}>Logout</button>
          </div>
        ) : (
          <LoginPage />
        )}
      </div>
    </>
  )
}

export default App
