import style from './StockData.module.css';
import { DataRow } from '../DataRow/DataRow';

const createRow = (item: any, index: any) => {
    return <DataRow key={index} symbol={item.symbol} price={item.lastPrice} />;
  };


export const StockData = () => {
    return(
        <div className={style.box}>
            <h1>Stocks</h1>
            {/* {data.map(createRow)} */}
          </div>
    );
}