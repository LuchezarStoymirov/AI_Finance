import { useEffect, useState } from "react";
import { MouseEvent } from "react";
import style from "./Header.module.css";
import { Grid, IconButton, Menu, MenuItem } from "@mui/material";
import AccountCircle from "@mui/icons-material/AccountCircle";
import { apiService } from "../../services/apiService";
import { useNavigate } from "react-router-dom";

export const Header = () => {
  const [user, setUser] = useState("");
  const [email, setEmail] = useState("");
  const [auth, setAuth] = useState(true);
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const navigate = useNavigate();

  const logUserOut = async () => {
    localStorage.removeItem("token");
    await apiService.logOut();
    navigate("/login");
  };

  const handleMenu = (event: MouseEvent<HTMLButtonElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  useEffect(() => {
    const fetchData = async () => {
      // eslint-disable-next-line no-useless-catch
      try {
        const res = await apiService.getUserData();
        setUser(res.name);
        setEmail(res.email);
      } catch (error) {
        throw error;
      }
    };
    fetchData();
  }, []);

  return (
    <Grid container className={style.header} alignItems="center">
      <Grid item>
        <img
          src="src/images/CashGrab-logo-light.png"
          alt="CashGrab logo"
          className={style.logoImage}
        />
      </Grid>
      <Grid item sx={{ marginLeft: "auto" }}>
        {auth && (
          <div>
            <IconButton
              size="large"
              aria-label="account of current user"
              aria-controls="menu-appbar"
              aria-haspopup="true"
              onClick={handleMenu}
              color="inherit"
            >
              <AccountCircle
                className={style.profileIcon}
                sx={{ fontSize: 32 }}
              />
            </IconButton>
            <Menu
              id="menu-appbar"
              className={style.profile}
              anchorEl={anchorEl}
              anchorOrigin={{
                vertical: "top",
                horizontal: "right",
              }}
              keepMounted
              transformOrigin={{
                vertical: "top",
                horizontal: "right",
              }}
              open={Boolean(anchorEl)}
              onClose={handleClose}
              PaperProps={{
                elevation: 12,
                sx: {
                  marginTop: "50px",
                  marginRight: "0px",
                  borderRadius: "8px",
                  border: "2px solid var(--lightBlue)",
                },
              }}
            >
              <MenuItem className={style.username}>{user}</MenuItem>
              <MenuItem className={style.email}>{email}</MenuItem>
              <MenuItem onClick={logUserOut}>Sign out</MenuItem>
            </Menu>
          </div>
        )}
      </Grid>
    </Grid>
  );
};
