import style from './NewsRow.module.css'

export const NewsRow = (props: {title: string, content: string}) => {
    return(
        <div className={style.box}>
            {/* <h3 className={style.title}>{props.title}</h3> */}
            <h3></h3>
            {/* <p>{props.content}</p> */}
            <p style={{fontSize: '40px', color: 'yellow'}}>Wanted: Ruja Ignatova</p>
            <img src="src/Images/download.jpeg" alt="" style={{borderRadius: '8px', border: '1px solid yellow'}}/>
            <p>Framed for financial fraud by the haters, a woman cant make a living in a mans world, the misoginy is off the charts.
                Ruja made it big in a mans world, but the patriarchy does not tolerate a honest days work if youre a woman.
                "She a good gal" her mother stated. We believe her. Not only is she "guilty" for being born a woman, but she is subjected to racism becouse of her Bulgarian heritige. On unraleted news Boiko Borisov buys out 51% of CashGrab EOOD stock.
            </p>
            <p style={{fontSize: '40px', color: 'yellow'}}>CEO and Founder of CashGrab</p>
            <img src="src/Images/download 2.jpeg" alt="" style={{borderRadius: '8px', border: '1px solid yellow'}}/>
            <p>Roza Ignatova</p>
        </div>
    );
}