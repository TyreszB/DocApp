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

  if (!user) {
    return <LoginPage />;
  }

  return (
    <div className="min-h-screen w-full flex flex-col bg-[#050805]">
      <NavBar />
      <main className="flex-1 w-full flex justify-center items-center px-4">
        <div className="text-center">
          <p className="text-[#b9ccb2]">Welcome, {user.name}!</p>
          <button
            onClick={handleLogout}
            className="mt-4 rounded-md border border-[#00ff41]/40 px-4 py-2 text-xs font-bold tracking-widest uppercase text-[#00ff41]"
          >
            Logout
          </button>
        </div>
      </main>
    </div>
  );
}

export default App
