import { Grid } from "@mui/material";

export const DataRow = (props) => {
  return (
    <div className="data-row">
      <Grid container rowSpacing={3} justifyContent="space-evenly">
        <Grid item xs={4} justifyContent="center">
          <h3 className="data-row__symbol">{props.symbol}</h3>
        </Grid>
        <Grid item xs={2} justifyContent="center" alignItems="center">
          <h3 className="data-row__price">{Math.round(props.price)}</h3>
        </Grid>
        <Grid item xs={2} justifyContent="center" alignItems="center">
          <h3 className="data-row__price">{Math.round(props.price)}</h3>
        </Grid>
        <Grid item xs={2} justifyContent="center" alignItems="center">
          <h3 className="data-row__price">{Math.round(props.price)}</h3>
        </Grid>
      </Grid>
    </div>
  );
};