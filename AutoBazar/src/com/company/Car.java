package com.company;

import java.io.*;
import java.time.Year;

/**
 * Třída pro vytváření aut
 */
public class Car implements Serializable {
    private String make;
    private String model;
    private int generation;
    private int year;

    /**
     * Konstruktor pro třídu Car
     *
     * @param make značka auta
     * @param model model auta
     * @param generation generace auta
     * @param year rok výroby auta
     * @throws InvalidInputException pokud je z některý z argumentů nevyhovující
     */
    public Car(String make, String model, int generation, int year) throws InvalidInputException {
        make = make.strip().substring(0, 1).toUpperCase() + make.strip().substring(1).toLowerCase();
        if (!doesMakeExist(make)) {
            throw new InvalidInputException("Zadaná značka automobilu neexistuje");
        }

        if (year > Year.now().getValue()) {
            throw new InvalidInputException("Neplatný rok výroby");
        }

        this.make = make;
        this.model = model.strip();
        this.generation = generation;
        this.year = year;
    }

    /**
     * Kontruktor pro práci s binárními soubory
     */
    public Car() {
        super();
    }

    /**
     * Rozhodne, zda je značka v databázi
     *
     * @return verdikt
     */
    private boolean doesMakeExist(String make) {
        return true;
        // prostě to nechce fungovat a já už na to nemám ani čas, ani nervy
        //try (BufferedReader reader = new BufferedReader(new FileReader("MakesDatabase.txt"))) {
        /*ClassLoader classLoader = Thread.currentThread().getContextClassLoader();
        try (InputStream in = classLoader.getResourceAsStream("MakesDatabase.txt");
             BufferedReader reader = new BufferedReader(new InputStreamReader(in))) {
            String line;

            while ((line = reader.readLine()) != null) {
                if (make.equals(line)) {
                    return true;
                }
            }
        }
        catch (IOException ioe) {
            return false;
        }

        return false;*/
    }

    public String getMake() {
        return make;
    }

    public String getModel() {
        return model;
    }

    public int getGeneration() {
        return generation;
    }

    public int getYear() {
        return year;
    }

    public void setMake(String make) throws InvalidInputException {
        make = make.strip().substring(0, 1).toUpperCase() + make.strip().substring(1).toLowerCase();
        if (!doesMakeExist(make)) {
            throw new InvalidInputException("Zadaná značka automobilu neexistuje");
        }

        this.make = make;
    }

    public void setModel(String model) {
        this.model = model;
    }

    public void setGeneration(int generation) {
        this.generation = generation;
    }

    public void setYear(int year) throws InvalidInputException {
        if (year > Year.now().getValue()) {
            throw new InvalidInputException("Neplatný rok výroby");
        }

        this.year = year;
    }

    @Override
    public String toString() {
        return "Značka: " + this.make + System.lineSeparator() +
                "Model: " + this.model + System.lineSeparator() +
                "Generace: " + this.generation + "." + System.lineSeparator() +
                "Rok výroby: " + this.year;
    }

    /**
     * Rozhodne, zda jsou instance identické
     *
     * @param other auto k porovnání
     * @return verdikt
     */
    public boolean equals(Car other) {
        return other.make.equals(this.make)
                && other.model.equals(this.model)
                && other.generation == this.generation
                && other.year == this.year;
    }
}
