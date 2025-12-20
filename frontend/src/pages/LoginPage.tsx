import { useAuth } from '../context/AuthContext';
import { AuthService } from '../services/auth.service';
import { useNavigate } from 'react-router-dom';
import { useState } from 'react';
import '../styles/login.css';

export const LoginPage = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const { login } = useAuth();
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');

    try {
      const { token, email: userEmail } = await AuthService.login(email, password);
      localStorage.setItem('token', token);

      login({ email: userEmail, name: userEmail.split('@')[0] });
      navigate('/projects');
    } catch (err) {
      setError('Invalid email or password');
      console.error(err);
    }
  };

  return (
    <div className="login-container">
      <div className="login-card">
        <h2>Login</h2>
        <form onSubmit={handleSubmit}>
          <input
            type="email"
            className="form-control"
            placeholder="Email"
            value={email}
            onChange={e => setEmail(e.target.value)}
            required
          />

          <input
            type="password"
            className="form-control"
            placeholder="Password"
            value={password}
            onChange={e => setPassword(e.target.value)}
            required
          />

          {error && <div className="text-danger">{error}</div>}

          <button type="submit" className="btn btn-primary w-100 btn-login">
            Login
          </button>
        </form>
      </div>
    </div>
  );
};
