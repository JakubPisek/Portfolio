package com.company;

import java.io.Serializable;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

/**
 * Třída pro vytváření inzerátů
 */
public class Advertisement implements Serializable {
    private Advertiser advertiser;
    private Car car;
    private String description;
    private String date;
    private float price;

    /**
     * Kontruktor pro třídu Advertisement
     *
     * @param advertiser instance třídy Advertiser
     * @param car instance třídy Car
     * @param description krátký doplňující popis vozidla
     * @param price cena za vozidlo
     */
    public Advertisement(Advertiser advertiser, Car car, String description, float price) {
        LocalDateTime actualDate = LocalDateTime.now();
        DateTimeFormatter formatActualDate = DateTimeFormatter.ofPattern("dd.MM.yyyy");
        String formattedDate = actualDate.format(formatActualDate);

        this.advertiser = advertiser;
        this.car = car;
        this.description = description.strip();
        this.date = formattedDate;
        this.price = price;
    }

    /**
     * Kontruktor pro práci s binárními soubory
     */
    public Advertisement() {
        super();
    }

    public Advertiser getAdvertiser() {
        return advertiser;
    }

    public Car getCar() {
        return car;
    }

    public String getDescription() {
        return description;
    }

    public String getDate() {
        return date;
    }

    public float getPrice() {
        return price;
    }

    public void setAdvertiser(Advertiser advertiser) {
        this.advertiser = advertiser;
    }

    public void setCar(Car car) {
        this.car = car;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public void setDate(String date) {
        this.date = date;
    }

    public void setPrice(float price) {
        this.price = price;
    }

    @Override
    public String toString() {
        return "PRODEJCE: " + System.lineSeparator() +
                this.advertiser.toString() + System.lineSeparator() +
                System.lineSeparator() +
                "AUTO: " + System.lineSeparator() +
                this.car.toString() + System.lineSeparator() +
                "POPIS: " + System.lineSeparator() +
                this.description + System.lineSeparator() +
                System.lineSeparator() +
                "Cena: " + this.price + " Kč" + System.lineSeparator() +
                "Datum přidání: " + this.date;
    }

    /**
     * Rozhodne, zda jsou instance identické
     *
     * @param other inzerát k porovnání
     * @return verdikt
     */
    public boolean equals(Advertisement other) {
        return other.advertiser.equals(this.advertiser) && other.car.equals(this.car);
    }
}
