import { useEffect, useState } from "react";
import { DataRow } from "../components/DataRow";

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
    <div style={{ background: '#293142', minHeight: '84.2vh' }}>
      <div style={{ display: 'flex', justifyContent: 'center', paddingTop: '40px' }}>
        <div style={{ width: '600px', background: '#1e242c', padding: '40px', borderRadius: '8px', boxShadow: '0 2px 4px rgba(0, 0, 0, 0.2)' }}>
          <h1 style={{ textAlign: 'center', marginBottom: '20px', color: '#61dafb', letterSpacing: '2px', textTransform: 'uppercase' }}>Data</h1>
          {data.map(createRow)}
        </div>
      </div>
    </div>
  );
};