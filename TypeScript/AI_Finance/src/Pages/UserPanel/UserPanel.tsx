import style from "./UserPanel.module.css";
import { apiService } from "../../services/apiService";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

export const UserPanel = () => {
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [selectedImage, setSelectedImage] = useState(
    "src/Images/profilepic.jpg"
  );
  const genericProfilePic = "src/Images/genericprofilepic.jpg";

  const navigate = useNavigate();

  const returnHome = () => {
    navigate('/');
  }

  useEffect(() => {
    const fetchData = async () => {
      // eslint-disable-next-line no-useless-catch
      try {
        const res = await apiService.getUserData();
        setUsername(res.name);
        setEmail(res.email);
      } catch (error) {
        throw error;
      }
    };
    fetchData();
  }, []);

  const handleImageUpload = (event: React.ChangeEvent<HTMLInputElement>) => {
    const file = event.target.files && event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = () => {
        setSelectedImage(reader.result as string);
      };
      reader.readAsDataURL(file);
    }
  };

  return (
    <div className={style.container}>
      <div className={style.box}>
        <div className={style.userinfo}>
          <img
            src={selectedImage || genericProfilePic}
            alt="Uploaded"
            className={style.profilePic}
          />
          </div>
          <div className={style.profileAttributes}>
          <input type="file" onChange={handleImageUpload} />
          <div className={style.usercell}>
          <h3>Username:</h3>
          <h4>{username}</h4>
          <p className={style.change}>Change</p>
          </div>
          <div className={style.usercell}>
          <h3>Email:</h3>
          <h4>{email}</h4>
          <p className={style.change}>Change</p>
          </div>
          </div>
          <div className={style.returndiv}>
          <button className={style.homebutton} onClick={returnHome}>Done</button>
          </div>
      </div>
    </div>
  );
};
