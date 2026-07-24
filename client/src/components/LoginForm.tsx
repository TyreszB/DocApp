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
    password: '',
  });

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSubmit(formData);
  };

  const labelClass =
    'text-[10px] font-bold tracking-widest uppercase text-[#6b8a64]';
  const inputClass =
    'w-full rounded-md border border-[#1f2e1c] bg-[#0d140f] px-3 py-2.5 text-sm text-[#c8dcc2] placeholder:text-[#3d4f38] outline-none transition focus:border-[#00ff41]/50 focus:ring-1 focus:ring-[#00ff41]/30 disabled:opacity-50';

  return (
    <form onSubmit={handleSubmit} className="w-full flex flex-col gap-5">
      <div className="flex flex-col gap-1.5">
        <label htmlFor="email" className={labelClass}>
          Email
        </label>
        <input
          type="email"
          id="email"
          name="email"
          placeholder="ENTER EMAIL"
          value={formData.email}
          onChange={handleInputChange}
          required
          disabled={isLoading}
          className={inputClass}
        />
      </div>

      <div className="flex flex-col gap-1.5">
        <label htmlFor="password" className={labelClass}>
          Password
        </label>
        <input
          type="password"
          id="password"
          name="password"
          placeholder="••••••••"
          value={formData.password}
          onChange={handleInputChange}
          required
          disabled={isLoading}
          className={inputClass}
        />
      </div>

      <button
        type="submit"
        disabled={isLoading}
        className="mt-2 w-full rounded-md bg-[#00ff41] py-3 text-xs font-bold tracking-[0.2em] uppercase text-[#050805] transition hover:bg-[#33ff66] disabled:cursor-not-allowed disabled:opacity-60"
      >
        {isLoading ? 'Logging in...' : 'Login'}
      </button>

      {error && (
        <p className="text-center text-xs tracking-wide text-red-400">{error}</p>
      )}
    </form>
  );
};

export default LoginForm;
