import { useState } from "react";
import { Link } from "react-router-dom"; // Import Link component
import { autservice } from "../../services/autService";
import style from './Login.module.css';

export const Login = () => {
  const [data, setData] = useState({
    email: "",
    password: "",
  });

  const handleSubmit = (e: any) => {
    const url = 'https://localhost:7085/api/login';
    e.preventDefault();
    try {
      const response =  autservice.login(url, data);
      console.log(response);
    } catch (error) {
      console.log(error);
    } 
  };

  return (
    <div className={style.container}>
      <form onSubmit={handleSubmit} className={style.form}>
        <img src="src\images\CashGrab-logo-light.png" alt="Login Image" className={style.image} />
        <h2 className={style.title}>Welcome</h2>
        <div className={style.registerInputContainer}>
          <input
            type="text"
            placeholder="Email"
            onChange={(e) => {
              setData({ ...data, email: e.target.value });
            }}
            className={style.input}
          />
        </div>
        <div className={style.registerInputContainer}>
          <input
            type="password"
            placeholder="Password"
            onChange={(e) => {
              setData({ ...data, password: e.target.value });
            }}
            className={style.input}
          />
        </div>
        <button type="submit" className={style.button}>Login</button>
        <p className={style.redirect}>
          Don't have an account? Sign up{" "}
          <Link to="/register" className={style.link}>Here</Link>.
        </p>
      </form>
    </div>
  );
};
