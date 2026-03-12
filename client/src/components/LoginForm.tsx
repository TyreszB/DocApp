import { useState } from 'react';

interface LoginFormData {
  email: string;
  password: string;
}

interface LoginFormProps {
  onSubmit: (data: LoginFormData) => void;
  isLoading?: boolean;
  error?: string | null;
  }

const LoginForm = ({ onSubmit, isLoading = false, error = null }: LoginFormProps) => {
  const [formData, setFormData] = useState<LoginFormData>({
    email: '',
    password: ''
  });

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: value
    }));
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSubmit(formData);
  };

  return (
      <form onSubmit={handleSubmit} className="w-full max-w-sm mx-auto">
      <div className="form-group flex flex-col gap-1 mb-4">
        <label htmlFor="email">Email:</label>
        <input
          type="email"
          id="email"
          name="email"
          value={formData.email}
          onChange={handleInputChange}
          required
          disabled={isLoading}
          className="w-full px-3 py-2 border rounded"
        />
      </div>
      <div className="form-group flex flex-col gap-1 mb-4">
        <label htmlFor="password">Password:</label>
        <input
          type="password"
          id="password"
          name="password"
          value={formData.password}
          onChange={handleInputChange}
          required
          disabled={isLoading}
          className="w-full px-3 py-2 border rounded"
        />
      </div>
      <button type="submit" disabled={isLoading} className="w-full py-2 px-4 rounded font-medium">
        {isLoading ? 'Logging in...' : 'Login'}
      </button>
      {error && <p className="error mt-2 text-red-600 text-sm">{error}</p>}
    </form>
  );
};

export default LoginForm; 