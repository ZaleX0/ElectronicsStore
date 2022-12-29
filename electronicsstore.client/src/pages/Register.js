import React, { useState } from 'react'
import { Button, Form } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../hooks/useAuth';
import AuthService from '../services/AuthService'

export default function Register() {
  const authService = new AuthService();
  const [formInputs, setFormInputs] = useState({ email: "", password: "" });
  const navigate = useNavigate();

  const handleChange = (event) => setFormInputs({ ...formInputs, [event.target.name]: event.target.value });

  const handleRegister = async (event) => {
    event.preventDefault();
    const response = await authService.register(formInputs.email, formInputs.password);
    
    if (response.status === 200) {
      navigate('/login');
    } else {
    }
  }

  return (
    <>
      <h1 className="text-center display-4 mb-4">Sign up</h1>
      <div className="d-flex justify-content-center">
        <Form className="w-25" onSubmit={handleRegister}>
          <Form.Group>
            <Form.Label>Email address</Form.Label>
            <Form.Control className="mb-2" type="email" placeholder="Enter email" name="email" value={formInputs.email} onChange={handleChange} required />
          </Form.Group>
          <Form.Group>
            <Form.Label>Password</Form.Label>
            <Form.Control className="mb-2" type="password" placeholder="Enter password" name="password" value={formInputs.password} onChange={handleChange} minLength={8} required />
          </Form.Group>
          <Button type="submit">Sign up</Button>
        </Form>
      </div>
    </>
  )
}
