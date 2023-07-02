import style from "./UserPanel.module.css";
import { apiService } from "../../services/apiService";
import { useEffect, useState } from "react";

export const UserPanel = () => {
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [selectedImage, setSelectedImage] = useState<File | null>(null);

  useEffect(() => {
    const fetchData = async () => {
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
      setSelectedImage(file);
    }
  };

  return (
    <div className={style.container}>
      <div className={style.box}>
        <h3>{username}</h3>
        <h3>{email}</h3>
        <input type="file" onChange={handleImageUpload} />
        {selectedImage && <img src={URL.createObjectURL(selectedImage)} alt="Uploaded" className={style.profilePic}/>}
      </div>
    </div>
  );
};
