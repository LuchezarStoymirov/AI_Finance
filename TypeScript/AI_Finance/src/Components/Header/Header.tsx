import { useState } from "react";
import style from './Header.module.css';
import { Grid, IconButton, Menu, MenuItem, Paper } from "@mui/material";
import AccountCircle from "@mui/icons-material/AccountCircle";


export const Header = () => {
  const [auth, setAuth] = useState(true);
  const [anchorEl, setAnchorEl] = useState(null);

  const logUserOut = () => {
    localStorage.removeItem('token');
    window.location.href = '/login';
  }

  const handleMenu = (event: any) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

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
              <AccountCircle sx={{ fontSize: 32, color: 'white', border: '2px solid var(--lightBlue)', borderRadius: '50%' }} />
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
                  marginTop: '50px',
                  marginRight: '0px',
                  borderRadius: '8px',
                  border: '2px solid var(--lightBlue)'
                },
              }}
            >
              <MenuItem className={style.username}>Username goes here</MenuItem>
              <MenuItem className={style.email}>User email goes here</MenuItem>
              <MenuItem onClick={logUserOut}>Sign out</MenuItem>
            </Menu>
          </div>
        )}
      </Grid>
    </Grid>
  );
};
