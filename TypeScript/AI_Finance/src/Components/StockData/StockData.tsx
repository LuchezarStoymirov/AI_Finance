import style from "./StockData.module.css";
import { DataRow } from "../DataRow/DataRow";
import { apiService } from "../../services/apiService";
import { useState, useEffect } from "react";
import { SimulatedStockData } from "../../MockData/MockData";

export const StockData = () => {
  const [data, setData] = useState([]);

  // useEffect(() => {
  //   (async () => {
  //     try {
  //       setData(await apiService.get());
  //     } catch (error) {
  //       // eslint-disable-next-line no-console
  //       console.log(error);
  //       throw error;
  //     }
  //   })();
  // }, []);  

  const createRow = (
    item: { symbol: string; price: number },
    index: number
  ) => {
    return <DataRow key={index} symbol={item.symbol} price={item.price} />;
  };

  return (
    <div className={style.box}>
      <div className={style.titlebox}>
        <h1 className={style.title}>Prices</h1>
      </div>
      {SimulatedStockData.map(createRow)}
    </div>
  );
};
