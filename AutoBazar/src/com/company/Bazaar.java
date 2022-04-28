package com.company;

import java.io.*;
import java.util.ArrayList;

/**
 * Třída pro vytváření bazarů
 *
 * @author Jakub Píšek
 */
public class Bazaar {
    private final String databaseFile;
    private final ArrayList<Advertisement> advertisements = new ArrayList<>();

    /**
     * Kontruktor pro třídu Bazaar
     *
     * @param binaryFile binární soubor, který poslouží jako databáze
     */
    public Bazaar(String binaryFile) {
        this.databaseFile = binaryFile;
    }

    /**
     * Výpis všech inzerátů v bazaru
     */
    public void printBazaar() {
        for (Advertisement advertisement : advertisements) {
            System.out.println("------------------------");
            System.out.println("ID: " + advertisements.indexOf(advertisement));
            System.out.println(advertisement);
        }
    }

    /**
     * Výpis všech inzerátů od konkrétního inzerenta
     *
     * @param advertiser konkrétní inzerent
     */
    public void printAdsFromAdvertiser(Advertiser advertiser) {
        int count = 0;

        for (Advertisement advertisement : advertisements) {
            if (advertisement.getAdvertiser().equals(advertiser)) {
                System.out.println("------------------------");
                System.out.println("ID: " + advertisements.indexOf(advertisement));
                System.out.println(advertisement);
                count++;
            }
        }

        System.out.println("--------------------");
        System.out.println("Celkem inzerátů od " + advertiser.getName() + ": " + count);
    }

    /**
     * Výpis všech inzerátů s auty od konkrétní značky
     *
     * @param make konkrétní značka
     */
    public void printAdsOfMake(String make) {
        make = make.strip().substring(0, 1).toUpperCase() + make.strip().substring(1).toLowerCase();
        int count = 0;

        for (Advertisement advertisement : advertisements) {
            if (advertisement.getCar().getMake().equals(make)) {
                System.out.println("------------------------");
                System.out.println("ID: " + advertisements.indexOf(advertisement));
                System.out.println(advertisement);
                count++;
            }
        }

        System.out.println("--------------------");
        System.out.println("Celkem vozidel značky " + make + ": " + count);
    }

    /**
     * Výpis všechn inzerátů v cenovém rozmezí
     *
     * @param lowerLimit spodní částka
     * @param upperLimit horní částka
     */
    public void printAdsInPriceRange(float lowerLimit, float upperLimit) {
        int count = 0;

        for (Advertisement advertisement : advertisements) {
            if (advertisement.getPrice() >= lowerLimit && advertisement.getPrice() <= upperLimit) {
                System.out.println("------------------------");
                System.out.println("ID: " + advertisements.indexOf(advertisement));
                System.out.println(advertisement);
                count++;
            }
        }

        System.out.println("--------------------");
        System.out.println("Celkem vozidel v cenovém rozmezí " + lowerLimit + " - " + upperLimit + ": " + count);
    }

    /**
     * Přidání inzerátu do bazaru
     *
     * @param advertisement inzerát k přidání
     * @return zda přidání probehlo
     */
    public boolean addAdvertisement(Advertisement advertisement) {
        if (advertisement == null) { return false; }

        for (Advertisement ad : advertisements) {
            if (ad.equals(advertisement)) {
                return false;
            }
        }

        advertisements.add(advertisement);
        return true;
    }

    /**
     * Smazání inzerátu z bazaru
     *
     * @param index index inzerátu ke smazání
     * @return zda odebrání probehlo
     */
    public boolean removeAdvertisement(int index) {
        try {
            advertisements.remove(index);
            return true;
        }
        catch (IndexOutOfBoundsException e) {
            return false;
        }
    }

    /**
     * Uložení databáze do binárního souboru
     *
     * @return zda uložení proběhlo
     */
    public boolean saveToFile() {
        try (ObjectOutputStream oos = new ObjectOutputStream(new FileOutputStream(databaseFile))) {
            for (Advertisement advertisement : advertisements) {
                oos.writeObject(advertisement);
            }
        }
        catch (IOException e) {
            return false;
        }

        return true;
    }

    /**
     * Načtení databáze z binárního souboru
     *
     * @return zda načtení proběhlo
     */
    public boolean loadFromFile() {
        try (ObjectInputStream ois = new ObjectInputStream(new FileInputStream(databaseFile))) {
            Object ad;

            while (true) {
                try {
                    ad = ois.readObject();
                    if (ad instanceof Advertisement) {
                        advertisements.add((Advertisement) ad);
                    }
                }
                catch (ClassNotFoundException e) {
                    return false;
                }
                catch (EOFException ex) {
                    break;
                }
            }
        }
        catch (IOException e) {
            System.out.println("Soubor nenalezen");
            return false;
        }

        return true;
    }
}
