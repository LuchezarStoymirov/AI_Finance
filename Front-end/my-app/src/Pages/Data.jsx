import { useEffect, useState } from "react";
import { DataRow } from "./DataRow";

export const Data = () => {
    const [data, setData] = useState([]);

    const fetchData = async () => {
        const response = await fetch('https://localhost:7085/Demo');
        const jsonData = await response.json();
        setData(jsonData);
    };

    useEffect(() => {
        fetchData();
    }, []);

    const createRow = (item, index) => {
        return (
            <DataRow 
                key={index}
                symbol={item.symbol}
                price={item.lastPrice}
            />
        );
    };

    console.log(data);

    return (
        <div className="data-div">
            <h1>Data</h1>
            {data.map(createRow)}
        </div>
    );
};