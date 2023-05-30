import { useState } from "react";

export const Login = () => {
  const [data, setData] = useState({
    email: "",
    password: "",
  });

  const handleSubmit = (e:any) => {
    console.log(data);
    // e.preventDefault();
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
      <div className="register-image-container">
        <img src="src\images\CashGrab-logo.png" alt="Login Image" className="register-image" />
      </div>
    </div>
  );
};
