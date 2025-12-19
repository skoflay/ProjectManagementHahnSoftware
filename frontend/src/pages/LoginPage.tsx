import { useAuth } from '../context/AuthContext';
import { AuthService } from '../services/auth.service';
import { useNavigate } from 'react-router-dom';
import { useState } from 'react';

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

    
    login({
      email: userEmail,
      name: userEmail.split('@')[0],
    });

    navigate('/projects');
  } catch (err) {
    setError('Invalid email or password');
    console.error(err);
  }
};


  return (
    <div className="container mt-5" style={{ maxWidth: 400 }}>
      <h2 className="mb-4">Login</h2>
      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label>Email</label>
          <input aria-label='g'
            type="email"
            className="form-control"
            value={email}
            onChange={e => setEmail(e.target.value)}
            required
          />
        </div>

        <div className="mb-3">
          <label>Password</label>
          <input aria-label='g'
            type="password"
            className="form-control"
            value={password}
            onChange={e => setPassword(e.target.value)}
            required
          />
        </div>

        {error && <div className="text-danger mb-2">{error}</div>}

        <button className="btn btn-primary w-100" type="submit">
          Login
        </button>
      </form>
    </div>
  );
};
