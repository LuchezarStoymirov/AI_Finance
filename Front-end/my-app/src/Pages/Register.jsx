import React from "react";
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import * as yup from 'yup';

export const Register = () => {
  const schema = yup.object().shape({
    user: yup.string().required(),
    email: yup.string().email().required(),
    age: yup.number().positive().integer().min(18).required(),
    password: yup.string().min(8).max(20).required(),
    confirmPassword: yup.string().oneOf([yup.ref("password"), null]).required()
  });

  const { register, handleSubmit } = useForm({
    resolver: yupResolver(schema)
  });

  const onSubmit = (data) => {
    console.log(data);
  };

  return (
    <div style={{ background: '#293142', minHeight: '100vh', display: 'flex', justifyContent: 'center', alignItems: 'center' }}>
      <form onSubmit={handleSubmit(onSubmit)} style={{ width: '400px', background: '#1e242c', padding: '40px', borderRadius: '8px', boxShadow: '0 2px 4px rgba(0, 0, 0, 0.2)', position: 'relative' }}>
        <h2 style={{ textAlign: 'center', marginBottom: '20px', color: '#61dafb', letterSpacing: '2px', textTransform: 'uppercase' }}>Registration Form</h2>
        <div style={{ marginBottom: '20px', display: 'flex', justifyContent: 'center' }}>
          <input type="text" placeholder="User" {...register("user")} className="form-control" style={{ width: '100%', padding: '10px', borderRadius: '4px', border: 'none', background: '#313942', color: '#fff' }} />
        </div>
        <div style={{ marginBottom: '20px', display: 'flex', justifyContent: 'center' }}>
          <input type="text" placeholder="Email" {...register("email")} className="form-control" style={{ width: '100%', padding: '10px', borderRadius: '4px', border: 'none', background: '#313942', color: '#fff' }} />
        </div>
        <div style={{ marginBottom: '20px', display: 'flex', justifyContent: 'center' }}>
          <input type="number" placeholder="Age" {...register("age")} className="form-control" style={{ width: '100%', padding: '10px', borderRadius: '4px', border: 'none', background: '#313942', color: '#fff' }} />
        </div>
        <div style={{ marginBottom: '20px', display: 'flex', justifyContent: 'center' }}>
          <input type="password" placeholder="Password" {...register("password")} className="form-control" style={{ width: '100%', padding: '10px', borderRadius: '4px', border: 'none', background: '#313942', color: '#fff' }} />
        </div>
        <div style={{ marginBottom: '20px', display: 'flex', justifyContent: 'center' }}>
          <input type="password" placeholder="Confirm Password" {...register("confirmPassword")} className="form-control" style={{ width: '100%', padding: '10px', borderRadius: '4px', border: 'none', background: '#313942', color: '#fff' }} />
        </div>
        <div style={{ textAlign: 'center' }}>
          <button type="submit" className="btn btn-primary" style={{ padding: '10px 20px', background: '#61dafb', color: '#fff', border: 'none', borderRadius: '4px', cursor: 'pointer' }}>Submit</button>
        </div>
      </form>
      <div style={{ position: 'absolute', top: '20px', left: '50%', transform: 'translateX(-50%)' }}>
        <img src="src\images\CashGrab-logo.png" alt="Registration Image" style={{ width: '400px', marginBottom: '20px' }} />
      </div>
    </div>
  );
};