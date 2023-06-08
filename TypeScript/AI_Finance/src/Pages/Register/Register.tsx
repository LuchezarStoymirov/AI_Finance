import style from "./Register.module.css";
import { useForm, SubmitHandler } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { autservice } from "../../services/autService";
import { Link } from "react-router-dom";
import * as yup from "yup";

interface FormData {
  user: string;
  email: string;
  password: string;
  confirmPassword: string;
}

export const Register = () => {
  const schema = yup.object().shape({
    user: yup.string().required("Field cannot be empty"),
    email: yup.string().email("Email must be valid").required("Field cannot be empty"),
    password: yup.string().required("Field cannot be empty"),
    confirmPassword: yup
      .string()
      .oneOf([yup.ref("password")], "Passwords must match")
      .required("Field cannot be  empty"),
  });

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<FormData>({
    resolver: yupResolver(schema),
  });

  const onSubmit: SubmitHandler<FormData> = async (data) => {
    const info = {
      name: data.user,
      email: data.email,
      password: data.password,
    };

    await autservice
      .register(info)
      .then(() => (window.location.href = "/login"))
      .catch((error) => {
        throw error;
      });
  };

  const hasErrors = Object.keys(errors).length > 0;

  return (
    <div className={style.container}>
      <form onSubmit={handleSubmit(onSubmit)} className={style.form}>
        <img
          src="src\images\CashGrab-logo-light.png"
          alt="Registration Image"
          className={style.image}
        />
        <h2 className={style.title}>Sign up</h2>
        <div className={style.registerInputContainer}>
          <input
            type="text"
            placeholder="User"
            {...register("user")}
            className={`${style.input} ${errors.user ? style.errorInput : ""} ${
              hasErrors ? style.shakeAnimation : ""
            }`}
          />
        </div>
        <p className={style.error}>{errors.user?.message}</p>
        <div className={style.registerInputContainer}>
          <input
            type="text"
            placeholder="Email"
            {...register("email")}
            className={`${style.input} ${
              errors.email ? style.errorInput : ""
            } ${hasErrors ? style.shakeAnimation : ""}`}
          />
        </div>
        <p className={style.error}>{errors.email?.message}</p>
        <div className={style.registerInputContainer}>
          <input
            type="password"
            placeholder="Password"
            {...register("password")}
            className={`${style.input} ${
              errors.password || errors.confirmPassword ? style.errorInput : ""
            } ${hasErrors ? style.shakeAnimation : ""}`}
          />
        </div>
        <p className={style.error}>{errors.password?.message}</p>
        <div className={style.registerInputContainer}>
          <input
            type="password"
            placeholder="Confirm Password"
            {...register("confirmPassword")}
            className={`${style.input} ${
              errors.confirmPassword ? style.errorInput : ""
            } ${hasErrors ? style.shakeAnimation : ""}`}
          />
        </div>
        <p className={style.error}>{errors.confirmPassword?.message}</p>
        <div className="register-button-container">
          <button type="submit" className={style.button}>
            Submit
          </button>
        </div>
        <p className={style.redirect}>
          Already have an account? Click{" "}
          <Link to="/login" className={style.link}>
            Here
          </Link>
          .
        </p>
      </form>
    </div>
  );
};
