    protected int pause = 0; //PAUSA PER VEURE LES TRANSICIONS D'ESTAT, 
    //QUAN ESTABLIM UN VALOR DE DEMORA MASSA ELEVAT EL PROGRAMA NO RESPON BE A LES POLSACIONS
    //MOLT CONSECUTIVES DE TECLES O PREMER VARIES A LA VEGADA    
    protected int direction;    //INDICADOR DE MOVEMENT. 1 = Amunt; 2 = Esquerra; 3 = Abaix; 4 = Dreta
    protected Random aleat = new Random();      //GENERADOR DE VALORS ALEATORIS
    protected int playing = 0;  //VARIABLE DE ESTAT QUE INDICARA SI LA PARTIDA ESTA EN CURS O NO
    
    /* OBJECTE DE CLASSE TIMER, QUE PERMET LLENÇAR UNA TASCA POSTERIORMENT A UN ESDEVENIMENT.
       ES IMPRESCINDIBLE SI VOLEM VEURE LES TRANSICIONS INTERNES DELS OBJECTES PER ARRIBAR AL SEU ESTAT FINAL
    */    
    protected Timer temporitzador = new Timer (); 
                    

    public void initGame() {                    
        //ASSIGNEM EL LISTENER DE TECLAT 
        play.setFocusable(true);
        play.setFocusCycleRoot(true);
        play.addKeyListener(this);
        fin.setVisible(false);
        scoreTextBox.setText("0");
        loadValue( aleat.nextInt(16)+1, 2 );       //GENEREM DIRECTAMENT UN 2
        selectEmptyBox();        
    }
    
     /**
     * metode que carrega un valor aleatoriament a una varialble del joc,
     * segons les regles, només pot ser 2 o 4
     */
    protected int setnewval () {
        int newval = aleat.nextInt(4)+1;
        
        //damos un delay a la generacion de nuevas casillas
        try {  //ENS OBLIGUEN A PROTEGIR-NOS AMB UN TRY           
            Thread.sleep(300);
        } catch (InterruptedException ex) {
            Logger.getLogger(JF_ProvesTemporitz.class.getName()).log(Level.SEVERE, null, ex);
        } 
        
        //COM QUE AL JOC REAL SURTEN MES 2 QUE 4... ALTERAREM LA PROPORCIO
        if (newval != 4){
            newval = 2;
        }                   //ARA EL 4 TE UN 25% DE PROBABILITATS I EL 2 UN 75%
        return newval;
    }
    
    /**
     * METODE QUE PERMET GENERAR UN VALOR ALEATORI ENTRE 2 I 4 TROBANT DE FORMA ALEATORIA
     * UNA CASELLA BUIDA. SI HO ACONSEGUEIX RETORNAR UN 0, SI NO POT PERQUE NO QUEDEN LLIURES RETORNA UN -1
     * @return 0 SI POT POSAR UN VALOR A UNA CASELLA LLIURE, SINO TROBA CAP RETORNA UN -1
     */
    public int selectEmptyBox () {   
        int maxintents = 100; //AMB 100 VEGADES SUPEREM DE SOBRES LES POSSIBILITATS DE TROBAR UNA CASELLA LLIURE
        int cont=0, error = -1, pos; 
        
        while (cont < maxintents && error==-1)  //SORTIREM PERQUE L'HEM TROBADA O PORQUE JA NO LA TROBAREM...
        {
            pos = aleat.nextInt(16)+1;
            switch (pos)  {
                case 1: if (button1.getText().equals("")){
                            error = 0;
                        }
                        break;
                case 2: if (button2.getText().equals("")){
                            error = 0;
                        }
                        break;
                case 3: if (button3.getText().equals("")){
                            error = 0;
                        }
                        break;
                case 4: if (button4.getText().equals("")){
                            error = 0;
                        }
                        break;
                case 5: if (button5.getText().equals("")){
                            error = 0;
                        }
                        break;
                case 6: if (button6.getText().equals("")){
                            error = 0;
                        }
                        break;
                case 7: if (button7.getText().equals("")){
                            error = 0;
                        }
                        break;
                case 8: if (button8.getText().equals("")){
                            error = 0;
                        }
                        break;
                case 9: if (button9.getText().equals("")){
                            error = 0;
                        }
                        break; 
                case 10: if (button10.getText().equals("")){
                            error = 0;
                        }
                        break;
                case 11: if (button11.getText().equals("")){
                            error = 0;
                        }
                        break;
                case 12: if (button12.getText().equals("")){
                            error = 0;
                        }
                        break;  
                case 13: if (button13.getText().equals("")){
                            error = 0;
                        }
                        break;
                case 14: if (button14.getText().equals("")){
                            error = 0;
                        }
                        break;
                case 15: if (button15.getText().equals("")){
                            error = 0;
                        }
                        break;
                case 16: if (button16.getText().equals("")){
                            error = 0;
                        }
                        break;   
            }
            if (error == 0)  //COM QUE JA HEM TROBAT UNA CAIXA BUIDA LI CARREGUEM EL VALOR
            {
                int value = setnewval();
                scoreTextBox.setText( String.valueOf( (value + Integer.valueOf(scoreTextBox.getText()) ) ) );
                loadValue( pos, value );      //afegim una unitat per generar valors entre 1 i 16
            } 
            else {
                cont++;
            }
        }
        if (error == -1){
            playing = 0;    
        }
        return error;
    }
        

    @Override
    public void keyPressed(KeyEvent e) {                             
        int tecla = e.getKeyCode();    //captura del codi ascii corresponent a la tecla polsada   
        /*
        //HEM DE DEFINIR QUE REALITZARA EL LLENÇADOR DE TASQUES QUE PROGRAMAREM
        TimerTask NewValue = new TimerTask() {  
            @Override
            public void run() {
                selectEmptyBox();          
            }
        };
        */
        
        direction = 0; //SINO ES TOCA UNA TECLA DE DESPLAÇAMENT ASIGNEM UN VALOR QUE NO IMPLICARA RES
        switch (tecla) {        
            case 87:        //CAP AMUNT
            case 38:
                direction =1;
                break;
            case 65:        //CAP A  L'ESQUERRA
            case 37:
                direction =2;
                break;
            case 83:        //CAP ABAIX
            case 40:
                direction =3;
                break;
            case 68:        //CAP A LA DRETA
            case 39:
                direction =4;
                break;
        }

        if (direction != 0){
            if ( moveBoxes() > 0 ) { //HA CONSEGUIDO MOVER UNA CAIXA          
                //temporitzador.schedule(NewValue, pause);  // LLENCEM LA TASCA
                selectEmptyBox(); 
            }                                                                                  
            if ( playing == 0 ) {      //JA NO QUEDEN CAIXES LLIURES           
                endGame();
            }
        }  
    }

    public void endGame()  {
        playing = 0;
        fin.setVisible(true);
    }
        
    /**
     * Metode que carregat un valor, i el color, de un objecte del panel
     * @param pos
     * @param value
    */
    protected void loadValue (int pos, int value){       
        Color color = Color.BLACK;  //color que asignarem als objectes
        String svalue = String.valueOf(value);
        Font myFont = null;
          
        button pButton=null;
        switch (pos) {
            case 1: pButton = button1;
                break;
            case 2: pButton = button2;
                break;
            case 3: pButton = button3;
                break;
            case 4: pButton = button4;
                break;
            case 5: pButton = button5;
                break;
            case 6: pButton = button6;
                break;
            case 7: pButton = button7;
                break;
            case 8: pButton = button8;
                break;
            case 9: pButton = button9;
                break;    
            case 10: pButton = button10;
                break;
            case 11: pButton = button11;
                break;
            case 12: pButton = button12;
                break;    
            case 13: pButton = button13;
                break;
            case 14: pButton = button14;
                break;
            case 15: pButton = button15;
                break;
            case 16: pButton = button16;                     
                break;    
        }
        pButton.setForeground(Color.BLACK); //por defecto el fondo es negro, con alguna excepcion que se trata especificamente
        switch (value){
            case 0: color = new Color (204, 204, 255);
                    //PER OBTENIR COLORS MES AGRADABLES ENLLOC DE TREBALLAR AMB ELS COLORS BASICS AWT, A LA PALETA GRAFICA
                    //ESCOLLIM EL COLOR QUE ENS AGRADI, ANEM A LA PESTANYA RGB I COPIEM
                    // ELS CODIS DELS COLORS RGB I ELS PASSEM COM A PARAMETRES DEL CONSTRUCTOR
                    // ARA JA PODEM CONFIGURAR UNS COLORS MES AGRAÏTS
                    myFont = new Font ("Arial", Font.BOLD, 32);  //A MIDA QUE EL VALOR TINGUI MES XIFRES CALDRA FER-LA MES PETITA
                    svalue = "";
                    break;                
            case 2: color = Color.LIGHT_GRAY;
                    myFont = new Font ("Arial", Font.BOLD, 32); 
                    break;
            case 4: color = Color.GRAY;
                    myFont = new Font ("Arial", Font.BOLD, 32); 
                    break;
            case 8: color = Color.WHITE;
                    myFont = new Font ("Arial", Font.BOLD, 32); 
                    break;
            case 16: color = Color.CYAN;
                    myFont = new Font ("Arial", Font.BOLD, 24); 
                    break;
            case 32: color = Color.GREEN;
                    myFont = new Font ("Arial", Font.BOLD, 24); 
                    break;
            case 64: color = Color.YELLOW;
                    myFont = new Font ("Arial", Font.BOLD, 24); 
                    break;
            case 128: color = Color.ORANGE;
                    myFont = new Font ("Arial", Font.BOLD, 18); 
                    break;
            case 256: color = Color.blue;
                    myFont = new Font ("Arial", Font.BOLD, 18);
                    pButton.setForeground(Color.WHITE);
                    break;
            case 512: color = Color.PINK;
                    myFont = new Font ("Arial", Font.BOLD, 18);
                    break;        
            case 1024: color = Color.RED;
                    myFont = new Font ("Arial", Font.BOLD, 16);
                    break;                
            case 2048: color = Color.MAGENTA;
                    myFont = new Font ("Arial", Font.BOLD, 16);
                    break;
            case 4096: color = Color.DARK_GRAY;
                    myFont = new Font ("Arial", Font.BOLD, 16);
                    break;    
            case 8192: color = Color.BLACK;
                    myFont = new Font ("Arial", Font.BOLD, 16);
                    pButton.setForeground(Color.WHITE);
                    break;
        }
               
        if (pButton != null){
            pButton.setText(svalue);
            pButton.setFont(myFont);
            pButton.setBackground(color);
        }                       
    }
          
    //PER MOURE LES CAIXES SEMPRE COMPROVAREM PRIMER QUE NO ESTIGUIN BUIDES
    //DESPRES MIRAREM SI EL SEU VALOR COINCIDEIX AMB EL DEL COSTAT EN EL SENTIT
    //MARCAT PER LA VARIABLE DIRECTION, SUMA EL VALORS AMB LA CAIXA DEL COSTAT
    //DEIXANT EL SEU VALOR BUIT. SINO REALITZA CAP MOVIMENT RETORNA 0 I NO ES GENERA VALOR
    protected int moveBoxes () {
        int moved = 0;            //MOVED INDICARA QUE HEM ACONSEGIT MOURE UNA CASELLA EN LA DIRECCIO INDICADA
                
        switch (direction) {
            case 1: // AMUNT  
                    moved += moveAndFuse (button1, button5, button9, button13, 1, 5, 9, 13);
                    moved += moveAndFuse (button2, button6, button10, button14, 2, 6, 10, 14);
                    moved += moveAndFuse (button3, button7, button11, button15, 3, 7, 11, 15);
                    moved += moveAndFuse (button4, button8, button12, button16, 4, 8, 12, 16); 
                    break;
            case 2: // ESQUERRA  
                    moved += moveAndFuse (button1, button2, button3, button4, 1, 2, 3, 4);
                    moved += moveAndFuse (button5, button6, button7, button8, 5, 6, 7, 8);
                    moved += moveAndFuse (button9, button10, button11, button12, 9, 10, 11, 12);
                    moved += moveAndFuse (button13, button14, button15, button16, 13, 14, 15, 16);
                break;   
            case 3: // ABAIX  
                    moved += moveAndFuse (button13, button9, button5, button1, 13, 9, 5, 1);
                    moved += moveAndFuse (button14, button10, button6, button2, 14, 10, 6, 2);
                    moved += moveAndFuse (button15, button11, button7, button3, 15, 11, 7, 3);
                    moved += moveAndFuse (button16, button12, button8, button4, 16, 12, 8, 4);  
                break;   
            case 4: // DRETA  
                    moved += moveAndFuse (button4, button3, button2, button1, 4, 3, 2, 1);
                    moved += moveAndFuse (button8, button7, button6, button5, 8, 7, 6, 5);
                    moved += moveAndFuse (button12, button11, button10, button9, 12, 11, 10, 9);
                    moved += moveAndFuse (button16, button15, button14, button13, 16, 15, 14, 13);                   
                break;                     
        }                          
        return moved;
    }
    
    //Recibe las cajas a mover/fusionar y sus codigos de posicion para reasignarles el valor 
    protected int moveAndFuse(button pButton1, button pButton2, button pButton3, button pButton4,
                              int pos1, int pos2, int pos3, int pos4)  {
        int moved = 0;
        int ivalue1, ivalue2, ivalue3, ivalue4;             //PREPAREM PER SI HEM DE MOURE VALORS
        //MOVE
        
        if (!pButton2.getText().equals("")) { //segona casella amb valor
            if (pButton1.getText().equals("")) { //i primera casella buida
                ivalue1 = Integer.valueOf(pButton2.getText()); //preparem valor per moure
                loadValue(pos1, ivalue1);
                loadValue(pos2, 0);
                moved = 1;
            }
        }
        if (!pButton3.getText().equals("")) { //tercera casella amb valor
            if (pButton2.getText().equals("")) { //segona casella buida
                if (pButton1.getText().equals("")) { //i primera casella buida
                    ivalue1 = Integer.valueOf(pButton3.getText()); //preparem valor per moure a la primera
                    loadValue(pos1, ivalue1);
                }else{
                    ivalue2 = Integer.valueOf(pButton3.getText()); //preparem valor per moure a la segona
                    loadValue(pos2, ivalue2);
                }    
                loadValue(pos3, 0);
                moved = 1;
            }
        }
        if (!pButton4.getText().equals("")) { //quarta casella amb valor
            if (pButton3.getText().equals("")) { //tercera casella buida
                if (pButton2.getText().equals("")) { //segona casella buida
                    if (pButton1.getText().equals("")) { //i primera casella buida
                        ivalue1 = Integer.valueOf(pButton4.getText()); //preparem valor per moure a la primera
                        loadValue(pos1, ivalue1);
                    }else{
                        ivalue2 = Integer.valueOf(pButton4.getText()); //preparem valor per moure a la segona
                        loadValue(pos2, ivalue2);
                    }    
                }else{
                    ivalue3 = Integer.valueOf(pButton4.getText()); //preparem valor per moure a la segona
                    loadValue(pos3, ivalue3);
                }
                loadValue(pos4, 0);
                moved = 1;
            }            
        }
    
        try {  //ENS OBLIGUEN A PROTEGIR-NOS AMB UN TRY
           
            Thread.sleep(100);
        } catch (InterruptedException ex) {
            Logger.getLogger(JF_ProvesTemporitz.class.getName()).log(Level.SEVERE, null, ex);
        } 
                
        // AND FUSE
        //SI ALMENYS TENIM 2 VALORS A LES PRIMERES CASELLES, ENTREM A COMPROVAR   
        if (!pButton1.getText().equals("") && !pButton2.getText().equals("") ) {  
            if ( pButton1.getText().equals(pButton2.getText())  ){  //TENEM EL MATEIX VALOR
                ivalue1 = 2 * Integer.valueOf(pButton1.getText());  //FUSIONEM VALORS
                loadValue(pos1, ivalue1);
                moved = 1;
                //ARA COMPROVEM SI A LES CASELLES 3 I 4 TENIM VALORS I SON IGUALS LES FUSIONEM SINO LES MOVEM
                if (!pButton3.getText().equals("") ){   //CASELLA 3 AMB VALOR
                    if (!pButton4.getText().equals("") ){//CADELLA 4 AMB VALOR
                        if ( pButton3.getText().equals(pButton4.getText())  ){  //TENEM EL MATEIX VALOR
                            ivalue2 = 2 * Integer.valueOf(pButton3.getText());  //FUSIONEM VALORS
                            loadValue(pos2, ivalue2);   //UNA CASELLA NOMES ES FUSIONA UNA VEGADA, I DEIXA ESPAI LLIURE
                            loadValue(pos3, 0);
                        }else{      //NO ES PODEN FUSIONAR NOMES CAL DESPLAÇAR
                            ivalue2 = Integer.valueOf(pButton3.getText());   // CARREGUEM EL SEU VALOR            
                            loadValue(pos2, ivalue2);
                            ivalue3 = Integer.valueOf(pButton4.getText());   // CARREGUEM EL SEU VALOR            
                            loadValue(pos3, ivalue3);
                        }
                        loadValue(pos4, 0);
                    }else{      //COM QUE LA 4 RESTA BUIDA NOMES CAL DESPLAÇAR LA 2
                        ivalue2 = Integer.valueOf(pButton3.getText());   // CARREGUEM EL SEU VALOR            
                        loadValue(pos2, ivalue2);
                        loadValue(pos3, 0);
                    }
                }else loadValue(pos2, 0); 
            }else {                 //TENEN DIFERENT VALOR, COMPROVEM LA CASELLAS 2 AMB LES CASELLES 3-4
                if (!pButton3.getText().equals("") ){
                    if ( pButton3.getText().equals(pButton2.getText())  ){  //TENEM EL MATEIX VALOR
                        ivalue2 = 2 * Integer.valueOf(pButton2.getText());  //FUSIONEM VALORS
                        loadValue(pos2, ivalue2);         //UNA CASELLA NOMES ES FUSIONA UNA VEGADA, I DEIXA ESPAI LLIURE
                        moved = 1;
                        if (!pButton4.getText().equals("") ){//COMPROVEM SI HEM DE MOURE CASELLA AL BUIT GENERAT
                            ivalue3 = Integer.valueOf(pButton4.getText());   // CARREGUEM EL SEU VALOR 
                            loadValue(pos3, ivalue3);
                            loadValue(pos4, 0);
                        }else
                            loadValue(pos3, 0);                             //BUIDEM NOMES LA CASELLA 3
                    }else{  //TENEN DIFERENT VALOR, COMPROVEM LES POSICIONS 3-4
                        if (!pButton4.getText().equals("") ){ //CASELLA 4 AMB VALOR
                            if ( pButton4.getText().equals(pButton3.getText()) ){  //TENEM EL MATEIX VALOR
                                ivalue3 = 2 * Integer.valueOf(pButton3.getText());  //FUSIONEM VALORS
                                loadValue(pos3, ivalue3);   
                                loadValue(pos4, 0);
                                moved = 1;
                            }    
                        } 
                    }
                }//SI LA CASELLA 3 RESTA BUIDA, LA 4 TAMBE, AIXI QUE JA NO TENIM RES A FUSIONAR
            }
        }//SINO TENIM LES PRIMERES CASELLES AMB VALORS NO TENIM RES A FUSIONAR
        
        return moved;
    }
    
    private void playMouseClicked(java.awt.event.MouseEvent evt) {//GEN-FIRST:event_playMouseClicked
        if (playing == 0){
            playing = 1; 
            fin.setVisible(false);
            initGame();            
        }                                   
    }