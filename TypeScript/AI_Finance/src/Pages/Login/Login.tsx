import { useState } from "react";
import { autservie } from "../../services/autService";
import style from './Login.module.css';

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
    <div className={style.container}>
      <form onSubmit={handleSubmit} className={style.form}>
      <div className={style.image}>
        <img src="src\images\CashGrab-logo-light.png" alt="Login Image" className={style.image} />
      </div>
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
            type="text"
            placeholder="Password"
            onChange={(e) => {
              setData({ ...data, password: e.target.value });
            }}
            className={style.input}
          />
        </div>
        <button type="submit" className={style.button}>Login</button>
      </form>
    </div>
  );
};
