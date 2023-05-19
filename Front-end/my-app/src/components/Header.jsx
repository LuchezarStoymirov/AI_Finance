import { Grid } from "@mui/material";
import { Link } from "react-router-dom";

export const Header = () => {
    return(
        <div>
            <Grid container sx={{backgroundColor: 'black', width: '100%', margin: '0'}} direction='row'>
                <Grid item>
                    <h1 style={{color: 'black'}}>Logo</h1>
                </Grid>
                <Grid container justifyContent='flex-end' alignItems='center' sx={{fontSize: '20px'}}>
                    <Grid item sx={{marginRight: '3%'}} > 
                        {<Link style={{color: 'white', textDecoration: 'none'}} to ='/'>Home</Link>}
                    </Grid>
                    <Grid item sx={{marginRight: '3%'}} >
                        {<Link style={{color: 'white', textDecoration: 'none'}} to = '/data'>Data</Link>}
                    </Grid>
                    <Grid item sx={{marginRight: '3%'}} >
                        {<Link style={{color: 'white', textDecoration: 'none'}} to ='/profile'>Profile</Link>}
                    </Grid>
                </Grid>
                
            </Grid>
        </div>
    );
}



