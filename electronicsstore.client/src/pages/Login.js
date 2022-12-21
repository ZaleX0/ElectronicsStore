import React, { useState } from 'react'
import { Button, Form } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../hooks/useAuth';
import AuthService from '../services/AuthService'

export default function Login() {
  const { login } = useAuth();
  const [formInputs, setFormInputs] = useState({ email: "", password: "" });
  const [isInvalid, setIsInvalid] = useState(false);
  const navigate = useNavigate();

  const handleChange = (event) => setFormInputs({ ...formInputs, [event.target.name]: event.target.value });

  const handleLogin = async (event) => {
    event.preventDefault();
    const response = await login(formInputs.email, formInputs.password);
    if (response.status === 200) {
      navigate('/');
    } else {
      setIsInvalid(true);
    }
  }

  return (
    <>
      <div className="text-center mb-4">
        <h1 className="display-4">Sign in</h1>
        <div>
          Don't have an account? <Link to="/register">Sign up</Link> now
        </div>
      </div>
      <div className="d-flex justify-content-center">
        <Form className="w-25" onSubmit={handleLogin}>
          <Form.Group>
            <Form.Label>Email address</Form.Label>
            <Form.Control className="mb-2" type="email" placeholder="Enter email" name="email" value={formInputs.email} onChange={handleChange} required />
          </Form.Group>
          <Form.Group>
            <Form.Label>Password</Form.Label>
            <Form.Control className="mb-2" type="password" placeholder="Enter password" name="password" value={formInputs.password} onChange={handleChange} required />
          </Form.Group>
          <Button type="submit">Sign in</Button>
          <div className="text-danger">
            {isInvalid && <p>Invalid email or password</p>}
          </div>
        </Form>
      </div>
    </>
  )
}
