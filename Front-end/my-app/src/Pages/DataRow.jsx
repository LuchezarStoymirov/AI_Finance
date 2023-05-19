import { Grid } from "@mui/material";

export const DataRow = (props) => {
    return(
        <div>
            <Grid container rowSpacing={1} columnSpacing={{ xs: 1, sm: 2, md: 3 }} justifyContent='space-evenly'>
                <Grid item xs="auto" justifyContent='center'>
                    <h3>{props.symbol}</h3>
                </Grid>
                <Grid item xs='auto' justifyContent='center'>
                    <h3>{Math.round(props.price)}</h3>
                </Grid>
                <Grid item xs='auto' justifyContent='center'>
                    <h3>{Math.round(props.price)}</h3>
                </Grid>
                <Grid item xs='auto' justifyContent='center'>
                    <h3>{Math.round(props.price)}</h3>
                </Grid>
            </Grid>
        </div>
    );
} 

//sm="auto" md= "auto" lg="auto" xl="auto"