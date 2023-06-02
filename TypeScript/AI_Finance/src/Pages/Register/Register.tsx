import style from './Register.module.css';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import { autservice } from '../../services/autService';
import * as yup from 'yup';



export const Register = () => {
  const schema = yup.object().shape({
    user: yup.string().required(),
    email: yup.string().email().required(),
    password: yup.string().required(),
    confirmPassword: yup.string().oneOf([yup.ref("password")]).required()
  });

  const { register, handleSubmit } = useForm({
    resolver: yupResolver(schema)
  });

  const onSubmit = (data:any) => {
    const url = 'https://localhost:7085/api/register'
    const info = {
        user: data.user,
        email: data.email,
        password: data.password
    }
    try {
        const response = autservice.register(url, info);
        console.log(response);
    } catch (error) {
        console.log(error);
        throw error;
    }
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
      </form>
    </div>
  );
};
