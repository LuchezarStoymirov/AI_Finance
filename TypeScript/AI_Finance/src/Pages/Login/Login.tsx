import { useEffect } from "react";
import { useState } from "react";
import { Link } from "react-router-dom";
import { autservice } from "../../services/autService";
import { config } from "../../Config/urlConfig";
import style from "./Login.module.css";

declare global {
  interface Window {
    google: any;
  }
}

export const Login = () => {
  const [data, setData] = useState({
    email: "",
    password: "",
  });

  const [token, setToken] = useState();

  const handleCallbackResponse = (response: any) => {
    console.log("Encoded JWT token:", response.credential);
  };

  useEffect(() => {
    window.google?.accounts.id.initialize({
      client_id:
        "477276107037-6nvps4ht1setgd3c4o4sao17fau71r17.apps.googleusercontent.com",
      callback: handleCallbackResponse,
    });

    window.google?.accounts.id.renderButton(
      document.getElementById("loginDiv"),
      { theme: "outline", size: "large" }
    );
  }, []);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    const url = config.baseURL + config.login;
    await autservice
      .login(url, data)
      .then((res) => setToken(res))
      .catch((error) => {throw error});
  };
  
  return (
    <div className={style.container}>
      <form onSubmit={handleSubmit} className={style.form}>
        <img
          src="src\images\CashGrab-logo-light.png"
          alt="Login Image"
          className={style.image}
        />
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
        <button type="submit" className={style.button}>
          Login
        </button>
        <p className={style.redirect}>
          Don't have an account? Sign up{" "}
          <Link to="/register" className={style.link}>
            Here
          </Link>
          .
        </p>
        <div id="loginDiv"></div>
      </form>
    </div>
  );
};
