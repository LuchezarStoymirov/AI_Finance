import { useEffect, useState } from "react";
import { useNavigate, Link } from "react-router-dom";
import { autservice } from "../../services/autService";
import { googleToken } from "./constants";
import style from "./Login.module.css";
import jwtDecode from "jwt-decode";

interface CallBack {
  credential: string;
}

interface GoogleLogin {
  email: string;
  name: string;
}

export const Login = () => {
  const [data, setData] = useState({
    email: "",
    password: "",
  });

  const navigate = useNavigate();

  const handleCallbackResponse = async (response: CallBack) => {
    const decoded_jwt: GoogleLogin = jwtDecode(response.credential);
    const info = {
      name: decoded_jwt.name,
      email: decoded_jwt.email,
      googleToken: response.credential,
    };
    await autservice
      .googleLogin(info)
      .then((res) => {
        localStorage.setItem("token", res.data.token);
        navigate("/");
      })
      .catch((error) => {
        throw error;
      });
  };

  useEffect(() => {
    window.google?.accounts.id.initialize({
      client_id: googleToken,
      callback: handleCallbackResponse,
    });

    window.google?.accounts.id.renderButton(document.getElementById("loginDiv"), {
      theme: "outline",
      size: "large",
    });
  }, []);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    await autservice
      .login(data)
      .then((res) => {
        localStorage.setItem("token", res.data.token);
        navigate("/");
      })
      .catch((error) => {
        throw error;
      });
  };

  return (
    <div className={style.container}>
      <form onSubmit={handleSubmit} className={style.form}>
        <img
          src="src/images/CashGrab-logo-light.png"
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
