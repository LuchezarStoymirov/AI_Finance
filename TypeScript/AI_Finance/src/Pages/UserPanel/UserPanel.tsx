import style from "./UserPanel.module.css";
import { apiService } from "../../services/apiService";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { pics } from "./constants";

interface Data {
  oldUsername: string;
  newUsername: string;
  oldEmail: string;
  newEmail: string;
}

export const UserPanel = () => {
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [selectedImage, setSelectedImage] = useState(pics.startingpic);
  const [shouldChangeName, setShouldChangeName] = useState(false);
  const [shouldChangeEmail, setShouldChangeEmail] = useState(false);
  const [newUsername, setNewUsername] = useState("");
  const [newEmail, setNewEmail] = useState("");

  const navigate = useNavigate();

  const nameHandler = () => {
    setShouldChangeName(!shouldChangeName);
  };

  const emailHandler = () => {
    setShouldChangeEmail(!shouldChangeEmail);
  };

  const returnHome = () => {
    navigate("/");
  };

  const closeModal = () => {
    setShouldChangeEmail(false);
    setShouldChangeName(false);
  };

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

  const sendNameData = () => {
    const data: Data = {
      oldUsername: username,
      newUsername: newUsername,
      oldEmail: email,
      newEmail: email,
    };
    apiService.changeUsername(data);
  };

  const sendEmailData = () => {
    const data: Data = {
      oldUsername: username,
      newUsername: username,
      oldEmail: email,
      newEmail: newEmail,
    };
    apiService.changeUsername(data);
  };

  return (
    <div className={style.container}>
      <div className={style.box}>
        <div className={style.userinfo}>
          <img
            src={selectedImage || pics.genericProfilePic}
            alt="Uploaded"
            className={style.profilePic}
          />
        </div>
        <div className={style.profileAttributes}>
          <input type="file" onChange={handleImageUpload} />
          <div className={style.usercell}>
            <h3>Username:</h3>
            <div className={style.datagroup}>
              <h4>{username}</h4>
              <button className={style.change} onClick={nameHandler}>
                {"\u270E"}
              </button>
            </div>
          </div>
          <div className={style.usercell}>
            <h3>Email:</h3>
            <div className={style.datagroup}>
              <h4>{email}</h4>
              <button className={style.change} onClick={emailHandler}>
                {"\u270E"}
              </button>
            </div>
          </div>
        </div>
        <div className={style.returndiv}>
          <button className={style.homebutton} onClick={returnHome}>
            Done
          </button>
        </div>
      </div>
      {shouldChangeName && (
        <div className={style.nameModal}>
          <div className={style.modalBox}>
            <div className={style.closediv}>
              <button className={style.closeButton} onClick={closeModal}>
                x
              </button>
            </div>
            <input
              type="text"
              placeholder="Enter new Username"
              className={style.valueInput}
              onChange={(event) => {
                setNewUsername(event.target.value);
              }}
            />
            <button className={style.submit} onClick={sendNameData}>
              Submit
            </button>
          </div>
        </div>
      )}
      {shouldChangeEmail && (
        <div className={style.nameModal}>
          <div className={style.modalBox}>
            <div className={style.closediv}>
              <button className={style.closeButton} onClick={closeModal}>
                x
              </button>
            </div>
            <input
              type="text"
              placeholder="Enter new Email"
              className={style.valueInput}
              onChange={(event) => {
                setNewEmail(event.target.value);
              }}
            />
            <button className={style.submit} onClick={sendEmailData}>
              Submit
            </button>
          </div>
        </div>
      )}
    </div>
  );
};
