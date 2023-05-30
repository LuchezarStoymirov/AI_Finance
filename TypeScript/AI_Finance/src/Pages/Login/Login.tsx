import { useState } from "react";
import axios from "axios";

export const Login = () => {

  const url = 'https://localhost:7085/api/Account/login'
  const [data, setData] = useState({
    email: "",
    password: "",
  });

  const handleSubmit = (e:any) => {
    e.preventDefault();
    axios.post(url, {
      username: data.email,
      password: data.password
    }).then( res => {
      console.log(res.data)
    }
    )
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
