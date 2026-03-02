package com.hilashmuel.hack.myapplication;

import android.annotation.SuppressLint;
import android.os.AsyncTask;
import android.os.Build;
import android.util.Log;

import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.InetAddress;

public class UDP_Client {
    private AsyncTask<Void, Void, Void> async_cient;
    public String Message;

    private InetAddress BroadcastAddress;
    private int SERVER_PORT = 9999;
    public String SERVERNAME;


    @SuppressLint("NewApi")
    public void NachrichtSenden() {
        async_cient = new AsyncTask<Void, Void, Void>() {
            @Override
            protected Void doInBackground(Void... params) {
                DatagramSocket ds = null;
                SERVERNAME = "132.68.34.255";

                try {
                    BroadcastAddress = InetAddress.getByName(SERVERNAME);

                    ds = new DatagramSocket();
                    DatagramPacket dp;
                    dp = new DatagramPacket(Message.getBytes(), Message.length());//, BroadcastAddress, SERVER_PORT);
                    dp.setPort(SERVER_PORT);
                    dp.setAddress(BroadcastAddress);
                    ds.setBroadcast(true);
                    ds.send(dp);
                } catch (Exception e) {
                    e.printStackTrace();
                } finally {
                    if (ds != null) {
                        ds.close();
                    }
                }
                return null;
            }

            protected void onPostExecute(Void result) {
                super.onPostExecute(result);
            }
        };

        if (Build.VERSION.SDK_INT >= 11)
            async_cient.executeOnExecutor(AsyncTask.THREAD_POOL_EXECUTOR);
        else async_cient.execute();
    }
}