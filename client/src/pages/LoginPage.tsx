import axios from 'axios';
import LoginForm from '../components/LoginForm';
import { useState } from 'react';

interface LoginFormData {
  email: string;
  password: string;
}

interface LoginResponse {
  userId: string;
  token: string;
  refreshToken: string;
}

const LoginPage = () => {

  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const handleSubmit = async (data: LoginFormData) => {
   
    try {
      setIsLoading(true);
      setError(null);
      const response = await axios.post<LoginResponse>('https://localhost:5001/api/auth/login', data);
      console.log('Login successful:', response.data);
      
      // Store the token and user ID in localStorage
      localStorage.setItem('token', response.data.token);
      localStorage.setItem('userId', response.data.userId);
      
      // Reload the page to trigger the authentication check
      window.location.reload();
    } catch (error) {
      console.error('Login failed:', error);
      setError('Invalid email or password');
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <LoginForm onSubmit={handleSubmit} isLoading={isLoading} error={error} />
  );
};

export default LoginPage; 