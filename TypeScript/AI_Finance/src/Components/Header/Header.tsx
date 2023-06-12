import { useState } from "react";
import { Grid, IconButton, Menu, MenuItem } from "@mui/material";
import AccountCircle from "@mui/icons-material/AccountCircle";
import "./Header.css";

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
    <Grid container className="header" alignItems="center">
      <Grid item>
        <img
          src="src/images/CashGrab-logo-light.png"
          alt="CashGrab logo"
          className="logo-image"
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
              <AccountCircle sx={{ fontSize: 32, color: 'white' }} />
            </IconButton>
            <Menu
              id="menu-appbar"
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
            >
              <MenuItem>Username goes here</MenuItem>
              <MenuItem>User email goes here</MenuItem>
              <MenuItem onClick={logUserOut}>Sign out</MenuItem>
            </Menu>
          </div>
        )}
      </Grid>
    </Grid>
  );
};
