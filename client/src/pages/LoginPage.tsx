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
      const response = await axios.post<LoginResponse>(
        'https://localhost:5001/api/auth/login',
        data
      );

      localStorage.setItem('token', response.data.token);
      localStorage.setItem('userId', response.data.userId);

      window.location.reload();
    } catch (err) {
      console.error('Login failed:', err);
      setError('Invalid email or password');
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="min-h-screen w-full flex flex-col items-center justify-center px-4 bg-[#050805]">
      <div className="w-full max-w-sm rounded-xl border border-[#00ff41]/40 bg-[#0a120c]/90 p-8 shadow-[0_0_40px_-16px_rgba(0,255,65,0.25)]">
        <div className="mb-8 text-center">
          <h1 className="text-2xl font-bold tracking-[0.2em] text-[#00ff41] uppercase">
            DocApp
          </h1>
          <p className="mt-2 text-[10px] tracking-widest uppercase text-[#6b8a64]">
            Systems operational
          </p>
        </div>

        <LoginForm onSubmit={handleSubmit} isLoading={isLoading} error={error} />
      </div>
    </div>
  );
};

export default LoginPage;
