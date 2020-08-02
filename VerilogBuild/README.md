# Verilog Build

## ULX3S

Assumes `fujprog` is in `c:\workspace\fujprog\build\fujprog.exe`

## Orange Crab

Be sure to hold down the button when plugging in!

`C:\workspace\dfu-util\bin-win64>dfu-util.exe`

See [foboot/tree/OrangeCrab](https://github.com/gregdavill/foboot/tree/OrangeCrab)


### List DFU Devices
```
C:\workspace\dfu-util\bin-win64\dfu-util.exe -l
```
should have output like:
```
dfu-util 0.9

Copyright 2005-2009 Weston Schmidt, Harald Welte and OpenMoko Inc.
Copyright 2010-2016 Tormod Volden and Stefan Schmidt
This program is Free Software and has ABSOLUTELY NO WARRANTY
Please report bugs to http://sourceforge.net/p/dfu-util/tickets/

Found DFU: [1209:5af0] ver=0101, devnum=20, cfg=1, intf=0, path="1-1", alt=0, name="OrangeCrab r0.2 DFU Bootloader v3.1", serial="UNKNOWN"

```

### VerilogBuild Program

```
cd C:\workspace\msbuildCustomTask\VerilogBuild
C:\workspace\dfu-util\bin-win64\dfu-util.exe -D boards\orangecrab\blink.dfu
```
should have output like:

```
dfu-util 0.9

Copyright 2005-2009 Weston Schmidt, Harald Welte and OpenMoko Inc.
Copyright 2010-2016 Tormod Volden and Stefan Schmidt
This program is Free Software and has ABSOLUTELY NO WARRANTY
Please report bugs to http://sourceforge.net/p/dfu-util/tickets/

Match vendor ID from file: 1209
Match product ID from file: 5af0
Opening DFU capable USB device...
ID 1209:5af0
Run-time device DFU version 0101
Claiming USB DFU Interface...
Setting Alternate Setting #0 ...
Determining device status: state = dfuIDLE, status = 0
dfuIDLE, continuing
DFU mode device DFU version 0101
Device returned transfer size 4096
Copying data from PC to DFU device
Download        [=========================] 100%        99314 bytes
Download done.
state(7) = dfuMANIFEST, status(0) = No error condition is present
state(8) = dfuMANIFEST-WAIT-RESET, status(0) = No error condition is present
Done!
```

