import style from "./StockData.module.css";
import { DataRow } from "../DataRow/DataRow";
import { SimulatedStockData } from "../../MockData/MockData";

export const StockData = () => {
  const createRow = (item: { symbol: string; price: number }, id: number) => {
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
