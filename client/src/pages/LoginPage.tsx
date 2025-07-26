import axios from 'axios';
import LoginForm from '../components/LoginForm';
import { useState } from 'react';

interface LoginFormData {
  username: string;
  password: string;
}

const LoginPage = () => {

  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const handleSubmit = async (data: LoginFormData) => {
   
    try {
      setIsLoading(true);
      // TODO: Replace with your actual login endpoint
      const response = await axios.post('https://localhost:5001/api/auth/login', data);
      console.log('Login successful:', response.data);
      // TODO: Handle successful login (redirect, store token, etc.)
    } catch (error) {
      console.error('Login failed:', error);
      setError('Invalid username or password');
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <LoginForm onSubmit={handleSubmit} isLoading={isLoading} error={error} />
  );
};

export default LoginPage; 