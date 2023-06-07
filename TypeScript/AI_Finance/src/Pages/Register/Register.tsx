import style from './Register.module.css';
import { useForm, SubmitHandler } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import { autservice } from '../../services/autService';
import { Link } from 'react-router-dom';
import { config } from '../../Config/urlConfig';
import * as yup from 'yup';

interface FormData {
  user: string;
  email: string;
  password: string;
  confirmPassword: string;
}

export const Register = () => {
  const schema = yup.object().shape({
    user: yup.string().required(),
    email: yup.string().email().required(),
    password: yup.string().required(),
    confirmPassword: yup.string().oneOf([yup.ref("password")]).required()
  });

  const { register, handleSubmit } = useForm<FormData>({
    resolver: yupResolver(schema)
  });

  const onSubmit: SubmitHandler<FormData> = async (data) => {
    const url = config.baseURL + config.register;
    const info = {
      name: data.user,
      email: data.email,
      password: data.password
    };
  
    const response = await autservice.register(url, info);
    return response;
  };
  
  

  return (
    <div className={style.container}>
      <form onSubmit={handleSubmit(onSubmit)} className={style.form}>
        <img src="src\images\CashGrab-logo-light.png" alt="Registration Image" className={style.image} />
        <h2 className={style.title}>Sign up</h2>
        <div className={style.registerInputContainer}>
          <input type="text" placeholder="User" {...register("user")} className={style.input} />
        </div>
        <div className={style.registerInputContainer}>
          <input type="text" placeholder="Email" {...register("email")} className={style.input} />
        </div>
        <div className={style.registerInputContainer}>
          <input type="password" placeholder="Password" {...register("password")} className={style.input} />
        </div>
        <div className={style.registerInputContainer}>
          <input type="password" placeholder="Confirm Password" {...register("confirmPassword")} className={style.input} />
        </div>
        <div className="register-button-container">
          <button type="submit" className={style.button}>Submit</button>
        </div>
        <p className={style.redirect}>
          Already have an account? Click{" "}
          <Link to='/login' className={style.link}>Here</Link>.
        </p>
      </form>
    </div>
  );
};
