import style from "./StockData.module.css";
import { DataRow } from "../DataRow/DataRow";
// import { apiService } from "../../services/apiService";
// import { useState, useEffect } from "react";
import { SimulatedStockData } from "../../MockData/MockData";

export const StockData = () => {
  // const [data, setData] = useState([]);

  //TODO
  // useEffect(() => {
  //   (async () => {
  //     try {
  //       setData(await apiService.getStockData());
  //     } catch (error) {
  //       // eslint-disable-next-line no-console
  //       console.log(error);
  //       throw error;
  //     }
  //   })();
  // }, []);  

  const createRow = (
    item: { symbol: string; price: number },
    id: number
  ) => {
    return <DataRow key={id} symbol={item.symbol} price={item.price} />;
  };

  return (
    <div className={style.box}>
      <div className={style.titlebox}>
        <h1 className={style.title}>Prices</h1>
        <div className={style.header}>
          <h4>Symbol</h4>
          <h4>Last Price</h4>
          <h4>Change</h4>
          <h4>% Change</h4>
        </div>
      </div>
      {SimulatedStockData.map(createRow)}
    </div>
  );
};
