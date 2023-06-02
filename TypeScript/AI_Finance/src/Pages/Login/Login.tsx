import { useState } from "react";
import { autservie } from "../../services/autService";

export const Login = () => {
  const [data, setData] = useState({
    email: "",
    password: "",
  });

  const handleSubmit = (e: any) => {
    e.preventDefault();
    try {
      const response =  autservie.login('https://localhost:7085/swagger/api/login', data);
      console.log(response);
    } catch (error) {
      console.log(error);
    } 
  };
  
  

  return (
    <div className="register-container">
      <form onSubmit={handleSubmit} className="register-form">
        <h2 className="register-title">Welcome</h2>
        <div className="register-input-container">
          <input
            type="text"
            placeholder="Email"
            onChange={(e) => {
              setData({ ...data, email: e.target.value });
            }}
            className="register-input"
          />
        </div>
        <div className="register-input-container">
          <input
            type="text"
            placeholder="Password"
            onChange={(e) => {
              setData({ ...data, password: e.target.value });
            }}
            className="register-input"
          />
        </div>
        <button type="submit" className="register-button">Login</button>
      </form>
    </div>
  );
};
