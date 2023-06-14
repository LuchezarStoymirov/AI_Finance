import { Grid } from "@mui/material";
import style from "./DataRow.module.css";


export const DataRow = (props: {symbol: string, price: number}) => {
  return (
    <div className={style.row}>
      <Grid container rowSpacing={3} justifyContent="space-evenly">
        <Grid item xs={4} justifyContent="center">
          <h3 className={style.symbol}>{props.symbol}</h3>
        </Grid>
        <Grid item xs={2} justifyContent="center" alignItems="center">
          <h3 className={style.price}>{Math.round(props.price)}</h3>
        </Grid>
        <Grid item xs={2} justifyContent="center" alignItems="center">
          <h3 className={style.price}>{Math.round(props.price)}</h3>
        </Grid>
        <Grid item xs={2} justifyContent="center" alignItems="center">
          <h3 className={style.price}>{Math.round(props.price)}</h3>
        </Grid>
      </Grid>
    </div>
  );
};
